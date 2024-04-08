
const delay = (ms) => new Promise((resolve) => setTimeout(resolve, ms));
async function renderRedSquareWithTextAndSubmit(context, device, redpipeline, renderPassDescriptorRed) {
    const redTexture = context.getCurrentTexture();
    renderPassDescriptorRed.colorAttachments[0].view = redTexture.createView();
    const encoderRed = device.createCommandEncoder({ label: 'our red encoder' });
    const redpass = encoderRed.beginRenderPass(renderPassDescriptorRed);
    redpass.setPipeline(redpipeline);

    // Отрисовываем красный квадрат с текстом

    redpass.draw(6);
    redpass.end();
    const commandBufferRed = encoderRed.finish();
    device.queue.submit([commandBufferRed]);
}
async function renderCircles(time,
    changingStorageBuffer,
    canvas,
    num,
    context,
    device,
    pipeline,
    renderPassDescriptor,
    objectInfos,
    changingUnitSize,
    kScaleOffset,
    vertexStorageBuffer,
    bindGroup,
    numVertices, storageValues) {
    //await delay(time);
    const textureCircle = context.getCurrentTexture();
    renderPassDescriptor.colorAttachments[0].view = textureCircle.createView();
    const encoderCircle = device.createCommandEncoder({ label: 'our circle encoder' });
    const passCircle = encoderCircle.beginRenderPass(renderPassDescriptor);
    passCircle.setPipeline(pipeline);

    const aspect = canvas.width / canvas.height;
    objectInfos.forEach(({ scale }, ndx) => {
        const offset = ndx * (changingUnitSize / 4);
        storageValues.set([scale / aspect, scale], offset + kScaleOffset);
    });
    device.queue.writeBuffer(changingStorageBuffer, 0, storageValues);
    passCircle.setBindGroup(0, bindGroup);
    passCircle.draw(numVertices, num);
    passCircle.end();
    const commandBufferCircle = encoderCircle.finish();
    device.queue.submit([commandBufferCircle]);
}
async function renderLinesWithDelay(time, numlinescount, context, device, pipeline, renderPassDescriptor) {
    const vertices = [];
    //await delay(time);
    const texture = context.getCurrentTexture();
    renderPassDescriptor.colorAttachments[0].view = texture.createView();
    const encoder = device.createCommandEncoder({ label: 'our line encoder' });
    const pass = encoder.beginRenderPass(renderPassDescriptor);
    pass.setPipeline(pipeline);
    for (let i = 0; i < numlinescount; ++i) {
        // Вычисляем координаты вершин линии
        const angle = (i / numlinescount) * Math.PI * 2;
 
        const endX = Math.round((Math.cos(angle) * 1000 )) ;
        const endY = Math.round((Math.sin(angle) * 500));

        // Добавляем координаты вершин в массив
        vertices.push( endX, endY);
    }
    pass.draw(numlinescount * 2);
    pass.end();
    const commandBuffer = encoder.finish();
    device.queue.submit([commandBuffer]);
    return vertices;
}

function createCircleVertices({
    radius = 1,
    numSubdivisions = 24,
    innerRadius = 0,
    startAngle = 0,
    endAngle = Math.PI * 2,
} = {}) {
    // 2 triangles per subdivision, 3 verts per tri, 2 values (xy) each.
    const numVertices = numSubdivisions * 3 * 2;
    const vertexData = new Float32Array(numSubdivisions * 2 * 3 * 2);

    let offset = 0;
    const addVertex = (x, y) => {
        vertexData[offset++] = x;
        vertexData[offset++] = y;
    };

    // Вычисляем границы для текущего круга и добавляем информацию о круге в массив circles
    const circle = {
        minX: Infinity,
        minY: Infinity,
        maxX: -Infinity,
        maxY: -Infinity,
        url: 'https://developer.mozilla.org/en-US/docs/Web/API/Canvas_API/Tutorial/Drawing_text',
    };

    for (let i = 0; i < numSubdivisions; ++i) {
        const angle1 = startAngle + (i + 0) * (endAngle - startAngle) / numSubdivisions;
        const angle2 = startAngle + (i + 1) * (endAngle - startAngle) / numSubdivisions;

        const c1 = Math.cos(angle1);
        const s1 = Math.sin(angle1);
        const c2 = Math.cos(angle2);
        const s2 = Math.sin(angle2);

        // first triangle
        const x1 = c1 * radius;
        const y1 = s1 * radius;
        const x2 = c2 * radius;
        const y2 = s2 * radius;
        addVertex(x1, y1);
        addVertex(x2, y2);
        addVertex(c1 * innerRadius, s1 * innerRadius);



        addVertex(c1 * innerRadius, s1 * innerRadius);
        addVertex(x2, y2);
        addVertex(c2 * innerRadius, s2 * innerRadius);




    }




    return {
        vertexData,
        numVertices,
    };
}

async function main(bibliographicMaterialCount) {
    const adapter = await navigator.gpu?.requestAdapter();
    const device = await adapter?.requestDevice();
    if (!device) {
        fail('need a browser that supports WebGPU');
        return;
    }

    const canvas = document.createElement('canvas');
    canvas.width = 1000; // Установите нужную ширину
    canvas.height = 1000; // Установите нужную высоту
    canvas.style.position = 'fixed';
    canvas.style.top = '50%';
    canvas.style.left = '50%';
    canvas.style.transform = 'translate(-50%, -50%)';
    document.body.appendChild(canvas);
    
    const context = canvas.getContext('webgpu');

    const presentationFormat = navigator.gpu.getPreferredCanvasFormat();
    context.configure({
        device,
        format: presentationFormat,
    });



    const modulecicle = device.createShaderModule({
        code: `
      struct OurStruct {
        color: vec4f,
        offset: vec2f,
      };

      struct OtherStruct {
        scale: vec2f,
      };

      struct Vertex {
        position: vec2f,
      };

      struct VSOutput {
        @builtin(position) position: vec4f,
        @location(0) color: vec4f,
      };

      @group(0) @binding(0) var<storage, read> ourStructs: array<OurStruct>;
      @group(0) @binding(1) var<storage, read> otherStructs: array<OtherStruct>;
      @group(0) @binding(2) var<storage, read> pos: array<Vertex>;

      @vertex fn vs(
        @builtin(vertex_index) vertexIndex : u32,
        @builtin(instance_index) instanceIndex: u32
      ) -> VSOutput {
        let otherStruct = otherStructs[instanceIndex];
        let ourStruct = ourStructs[instanceIndex];

        var vsOut: VSOutput;
        vsOut.position = vec4f(
            pos[vertexIndex].position * otherStruct.scale + ourStruct.offset, 0.0, 1.0);
        vsOut.color = ourStruct.color;
        return vsOut;
      }

      @fragment fn fs(vsOut: VSOutput) -> @location(0) vec4f {
        return vsOut.color;
      }
    `,
    });




    const numLines = bibliographicMaterialCount;

    const moduleLine = device.createShaderModule({
        label: 'our hardcoded line shaders',
        code: `
      @vertex fn vs(
        @builtin(vertex_index) vertexIndex : u32
      ) -> @builtin(position) vec4f {
        // Calculate the angle for each line with equal spacing
        let angle = (f32(vertexIndex) / f32(${numLines})) * 6.283185;  // 6.283185 is 2 * PI

        // Fixed center point
        let centerX = 0.0;
        let centerY = 0.0;

        // Calculate line position from the fixed center with a radius of 0.5
        let radius = 0.9;
        let posX = centerX + cos(angle) * radius;
        let posY = centerY + sin(angle) * radius;

        return vec4f(posX, posY, 0.0, 1.0);
      }
    `,
    });

    const redmodule = device.createShaderModule({
        label: 'our hardcoded red triangle shaders',
        code: `
      @vertex fn vs(
        @builtin(vertex_index) vertexIndex : u32
      ) -> @builtin(position) vec4f {
        let pos = array(
            vec2<f32>(-0.1, -0.1), // Левый нижний угол
                vec2<f32>(0.1, -0.1),  // Правый нижний угол
                vec2<f32>(-0.1, 0.1),  // Левый верхний угол
                vec2<f32>(-0.1, 0.1),  // Левый верхний угол (повторение для закрытия квадрата)
                vec2<f32>(0.1, -0.1),  // Правый нижний угол
                vec2<f32>(0.1, 0.1),   // Правый верхний угол
        );

        return vec4f(pos[vertexIndex], 0.0, 1.0);
      }
    `,
    });


    const moduleLineCenter = device.createShaderModule({
        label: 'our hardcoded line shaders',
        code: `
      @vertex fn vs(
        @builtin(vertex_index) vertexIndex : u32
      ) -> @builtin(position) vec4f {
        // Calculate the angle for each line with equal spacing
        let angle = (f32(vertexIndex) / f32(${numLines})) * 6.283185;  // 6.283185 is 2 * PI

        // Fixed center point
        let centerX = 0.0;
        let centerY = 0.0;

        // Calculate line position from the fixed center with a radius of 0.5
        let radius = 1.0;
        let posX = centerX + cos(angle) * radius * f32(vertexIndex % 2 );
        let posY = centerY + sin(angle) * radius * f32(vertexIndex % 2);

        return vec4f(posX, posY, 0.0, 1.0);
      }
    `,
    });



    const fragmentShaderModuleLineCenter = device.createShaderModule({
        code: `
           @fragment fn fs() -> @location(0) vec4f {
        return vec4f(1, 1, 0, 1);
      }
        `,
    });



    const fragmentShaderModuleLine = device.createShaderModule({
        code: `
           @fragment fn fs() -> @location(0) vec4f {
        return vec4f(1, 1, 0, 1);
      }
        `,
    });
    const redFragmentShaderModule = device.createShaderModule({
        code: `
           @fragment fn fs() -> @location(0) vec4f {
        return vec4f(1, 0, 0, 1);
      }
        `,
    });





    const pipelineLineCenter = device.createRenderPipeline({
        label: 'our hardcoded line pipeline',
        layout: 'auto',
        vertex: {
            module: moduleLineCenter,
            entryPoint: 'vs',
        },
        fragment: {
            module: fragmentShaderModuleLineCenter,
            entryPoint: 'fs',
            targets: [{ format: presentationFormat }],
        },
        primitive: {
            topology: 'line-list'
        },
    });



    const pipelineLine = device.createRenderPipeline({
        label: 'our hardcoded line pipeline',
        layout: 'auto',
        vertex: {
            module: moduleLine,
            entryPoint: 'vs',
        },
        fragment: {
            module: fragmentShaderModuleLine,
            entryPoint: 'fs',
            targets: [{ format: presentationFormat }],
        },
        primitive: {
            topology: 'line-list'
        },
    });

    const redpipeline = device.createRenderPipeline({
        label: 'our hardcoded red triangle pipeline',
        layout: 'auto',
        vertex: {
            module: redmodule,
            entryPoint: 'vs',
        },
        fragment: {
            module: redFragmentShaderModule,
            entryPoint: 'fs',
            targets: [{ format: presentationFormat }],
        },
    });







    const pipeline = device.createRenderPipeline({
        label: 'storage buffer vertices',
        layout: 'auto',
        vertex: {
            module: modulecicle,
            entryPoint: 'vs',
        },
        fragment: {
            module: modulecicle,
            entryPoint: 'fs',
            targets: [{ format: presentationFormat }],
        },
    });


    const kNumObjects = bibliographicMaterialCount || 10;
    const objectInfos = [];


    // create 2 storage buffers
    const staticUnitSize =
        4 * 4 + // color is 4 32bit floats (4bytes each)
        2 * 4 + // offset is 2 32bit floats (4bytes each)
        2 * 4;  // padding
    const changingUnitSize =
        2 * 4;  // scale is 2 32bit floats (4bytes each)
    const staticStorageBufferSize = staticUnitSize * kNumObjects;
    const changingStorageBufferSize = changingUnitSize * kNumObjects;

    const staticStorageBuffer = device.createBuffer({
        label: 'static storage for objects',
        size: staticStorageBufferSize,
        usage: GPUBufferUsage.STORAGE | GPUBufferUsage.COPY_DST,
    });

    const changingStorageBuffer = device.createBuffer({
        label: 'changing storage for objects',
        size: changingStorageBufferSize,
        usage: GPUBufferUsage.STORAGE | GPUBufferUsage.COPY_DST,
    });


    const kColorOffset = 0;
    const kOffsetOffset = 4;

    const kScaleOffset = 0;

    {
        const staticStorageValues = new Float32Array(staticStorageBufferSize / 4);
        const fixedRadius = 0.9;
        const fixedScale = 0.2;

        for (let i = 0; i < kNumObjects; ++i) {
            const staticOffset = i * (staticUnitSize / 4);


            staticStorageValues.set([1.0, 0.0, 0.0, 1.0], staticOffset + kColorOffset);
            const angle = (i / kNumObjects) * Math.PI * 2;


            const offsetX = Math.cos(angle) * fixedRadius;
            const offsetY = Math.sin(angle) * fixedRadius;

            staticStorageValues.set([offsetX, offsetY], staticOffset + kOffsetOffset);

            objectInfos.push({
                scale: fixedScale,
            });

        }
        device.queue.writeBuffer(staticStorageBuffer, 0, staticStorageValues);
    }


    const storageValues = new Float32Array(changingStorageBufferSize / 4);


    const { vertexData, numVertices } = createCircleVertices({
        radius: 0.5,
        innerRadius: 0,
    });



    const vertexStorageBuffer = device.createBuffer({
        label: 'storage buffer vertices',
        size: vertexData.byteLength,
        usage: GPUBufferUsage.STORAGE | GPUBufferUsage.COPY_DST,
    });
    device.queue.writeBuffer(vertexStorageBuffer, 0, vertexData);




    const bindGroup = device.createBindGroup({
        label: 'bind group for objects',
        layout: pipeline.getBindGroupLayout(0),
        entries: [
            { binding: 0, resource: { buffer: staticStorageBuffer } },
            { binding: 1, resource: { buffer: changingStorageBuffer } },
            { binding: 2, resource: { buffer: vertexStorageBuffer } },
        ],
    });
    //const texture = context.getCurrentTexture();

    const renderPassDescriptor = {
        label: 'our combined canvas renderPass',
        colorAttachments: [
            {
                // view: texture.createView(),
                clearValue: [0.3, 0.3, 0.3, 1],
                loadOp: 'load',
                storeOp: 'store',
            },
        ],
    };

    /*Создаем текстуру для renderPassDescriptorLine*/

    const renderPassDescriptorLine = {
        label: 'our line canvas renderPass',
        colorAttachments: [
            {

                clearValue: [0.3, 0.3, 0.3, 1],
                loadOp: 'clear',
                storeOp: 'store',
            },
        ],
    };



    //// Создаем текстуру для renderPassDescriptorLineCenter

    //const renderPassDescriptorLineCenter = {
    //    label: 'our line center canvas renderPass',
    //    colorAttachments: [
    //        {

    //            clearValue: [0.3, 0.3, 0.3, 1],
    //            loadOp: 'load',
    //            storeOp: 'store',
    //        },
    //    ],
    //};

    const renderPassDescriptorRed = {
        label: 'our basic canvas renderPass',
        colorAttachments: [
            {

                clearValue: [0.3, 0.3, 0.3, 1],
                loadOp: 'clear',
                storeOp: 'store',
            },
        ],
    };


   //  mouseover
   //click 
    document.addEventListener('click', handleClick);

    // Функция для начала отображения всех элементов
    async function startRendering() {
        // Вызываем вашу функцию отрисовки здесь
        await render();
    }
    function handleClick(event) {
        const canvas = event.target;
        const rect = canvas.getBoundingClientRect();
        const x = event.clientX - rect.left;
        const y = event.clientY - rect.top;
        console.log(`Мышь  в (${x}, ${y})`);

        if (x >= 450 && y >= 450 && x <= 550 && y <= 550) {
            console.log('Начинаем отрисовку...');
         
            document.removeEventListener('click', handleClick);
            startRendering();

        }
        
    }
   
  

    async function StartRender() {
        renderRedSquareWithTextAndSubmit(context, device, redpipeline, renderPassDescriptorRed)
    }
   await  StartRender()

    async function render() {

       

        // Отрисовка кругов



        await renderLinesWithDelay(1000,
            bibliographicMaterialCount,
            context,
            device,
            pipelineLine,
            renderPassDescriptorLine);
     
        


        // Отрисовка линий


        const vertic =  await renderLinesWithDelay(10000,
            bibliographicMaterialCount,
            context,
            device,
            pipelineLineCenter,
            renderPassDescriptor);
        console.log(vertic);

        // Отрисовка линий

        await renderCircles(1000,
            changingStorageBuffer,
            canvas,
            bibliographicMaterialCount,
            context,
            device,
            pipeline,
            renderPassDescriptor,
            objectInfos,
            changingUnitSize,
            kScaleOffset,
            vertexStorageBuffer,
            bindGroup,
            numVertices,
            storageValues
        );

        await renderRedSquareWithTextAndSubmit(context,
            device,
            redpipeline,
            renderPassDescriptor)
      


    }


    //await render();




}

function fail(msg) {
    alert(msg);
}
var first = booksData.AllBibliographicmaterial;
main(first.length); 