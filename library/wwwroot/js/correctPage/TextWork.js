import { mat4 } from 'https://webgpufundamentals.org/3rdparty/wgpu-matrix.module.js';



const glyphWidth = 32;
const glyphHeight = 40;
const glyphsAcrossTexture = 16;
function formatBookInfo(author, date, name, publisher) {
    const formattedInfo = `Автор:${author}\n\nГод издания:${date}\n\nНазвание:${name}\n\nИздательство:${publisher}`;

    return formattedInfo;
}
function genreateGlyphTextureAtlas() {
    const ctx = document.createElement('canvas').getContext('2d');
    ctx.imageSmoothingEnabled = true;
    ctx.canvas.width = 512;
    ctx.canvas.height = 600;

    let x = 0;
    let y = 0;
    ctx.font = '20px Arial'; // Используйте шрифт Arial, который поддерживает кириллицу
    ctx.textBaseline = 'middle';
    ctx.textAlign = 'start';
    ctx.fillStyle = 'white';

    for (let c = 32; c <= 126; ++c) {
        ctx.fillText(String.fromCodePoint(c), x + glyphWidth / 2, y + glyphHeight / 2);
        x += glyphWidth;
        if (x >= ctx.canvas.width) {
            x = 0;
            y += glyphHeight;
        }
    }
    for (let c = 1040; c <= 1103; ++c) {
        ctx.fillText(String.fromCodePoint(c), x + glyphWidth / 2, y + glyphHeight / 2);
        x += glyphWidth;
        if (x >= ctx.canvas.width) {
            x = 0;
            y += glyphHeight;
        }
    }


    return ctx.canvas;
}


async function main(AllLibraryInfo) {
    const adapter = await navigator.gpu?.requestAdapter();
    const device = await adapter?.requestDevice();
    if (!device) {
        fail('need a browser that supports WebGPU');
        return;
    }

    // Создание canvas и его настройка
    const canvas = document.createElement('canvas');
    const submitButton = document.createElement('button');
    submitButton.textContent = 'Отправить'; // Устанавливаем текст на кнопке
    submitButton.style.position = 'absolute'; // Устанавливаем позиционирование
    submitButton.style.top = '35%'; // Устанавливаем отступ сверху
    submitButton.style.left = '3%'; // Устанавливаем отступ слева
    submitButton.style.display = 'none'; // Делаем кнопку невидимой

    const authorInput = document.createElement('input');
    authorInput.placeholder = 'Автор';
    authorInput.style.position = 'absolute';
    authorInput.style.top = '13%';
    authorInput.style.left = '3%';
    authorInput.style.display = 'none'; // Делаем поле ввода автора невидимым

    const yearInput = document.createElement('input');
    yearInput.placeholder = 'Год Издания';
    yearInput.style.position = 'absolute';
    yearInput.style.top = '18%';
    yearInput.style.left = '3%';
    yearInput.style.display = 'none'; // Делаем поле ввода года издания невидимым

    const nameInput = document.createElement('input');
    nameInput.placeholder = 'Название';
    nameInput.style.position = 'absolute';
    nameInput.style.top = '24%';
    nameInput.style.left = '3%';
    nameInput.style.display = 'none'; // Делаем поле ввода названия невидимым

    const publisherInput = document.createElement('input');
    publisherInput.placeholder = 'Издательство';
    publisherInput.style.position = 'absolute';
    publisherInput.style.top = '29%';
    publisherInput.style.left = '3%';
    publisherInput.style.display = 'none'; // Делаем поле ввода издательства невидимым


    canvas.style.backgroundColor = 'transparent'; // Устанавливаем прозрачный фон
    canvas.width = 300; // Установите нужную ширину
    canvas.height = 150; // Установите нужную высоту
  //  canvas.style.border = '1px solid black';
    canvas.style.position = 'absolute';
    canvas.style.top = '60%';
    canvas.style.left = '20%';
    canvas.style.transform = 'translate(-50%, -50%)';
    canvas.style.zIndex = '9999';

    document.body.appendChild(canvas);
    document.body.appendChild(authorInput);
    document.body.appendChild(yearInput);
    document.body.appendChild(nameInput);
    document.body.appendChild(publisherInput);
    document.body.appendChild(submitButton);

    const context = canvas.getContext('webgpu');
    const presentationFormat = navigator.gpu.getPreferredCanvasFormat();
    context.configure({
        device,
        format: presentationFormat,
    });



    const module = device.createShaderModule({
        label: 'our hardcoded textured quad shaders',
        code: `
      struct VSInput {
        @location(0) position: vec4f,
        @location(1) texcoord: vec2f,
        @location(2) color: vec4f,
      };

      struct VSOutput {
        @builtin(position) position: vec4f,
        @location(0) texcoord: vec2f,
        @location(1) color: vec4f,
      };

      struct Uniforms {
        matrix: mat4x4f,
      };

      @group(0) @binding(2) var<uniform> uni: Uniforms;

      @vertex fn vs(vin: VSInput) -> VSOutput {
        var vsOutput: VSOutput;
        vsOutput.position = uni.matrix * vin.position;
        vsOutput.texcoord = vin.texcoord;
        vsOutput.color = vin.color;
        return vsOutput;
      }

      @group(0) @binding(0) var ourSampler: sampler;
      @group(0) @binding(1) var ourTexture: texture_2d<f32>;

      @fragment fn fs(fsInput: VSOutput) -> @location(0) vec4f {
        return textureSample(ourTexture, ourSampler, fsInput.texcoord) * fsInput.color;
      }
    `,
    });




    const glyphCanvas = genreateGlyphTextureAtlas();
    // document.body.appendChild(glyphCanvas);// отображение карты всех символов 
    glyphCanvas.style.backgroundColor = '#222';

    const maxGlyphs = 10000;
    const floatsPerVertex = 2 + 2 + 4;
    const vertexSize = floatsPerVertex * 4;
    const vertsPerGlyph = 6;
    const vertexBufferSize = maxGlyphs * vertsPerGlyph * vertexSize;
    const vertexBuffer = device.createBuffer({
        label: 'vertices',
        size: vertexBufferSize,
        usage: GPUBufferUsage.VERTEX | GPUBufferUsage.COPY_DST,
    });
    const indexBuffer = device.createBuffer({
        label: 'indices',
        size: maxGlyphs * vertsPerGlyph * 4,
        usage: GPUBufferUsage.INDEX | GPUBufferUsage.COPY_DST,
    });


    {
        const indices = [];
        for (let i = 0; i < maxGlyphs; ++i) {
            const ndx = i * 4;
            indices.push(ndx, ndx + 1, ndx + 2, ndx + 2, ndx + 1, ndx + 3);
        }
        device.queue.writeBuffer(indexBuffer, 0, new Uint32Array(indices));
    }

    function generateGlyphVerticesForText(s, colors = [[1, 1, 1, 1]]) {
        const vertexData = new Float32Array(maxGlyphs * floatsPerVertex * vertsPerGlyph);
        const glyphUVWidth = glyphWidth / glyphCanvas.width;
        const glyphUVheight = glyphHeight / glyphCanvas.height;
        let offset = 0;
        let x0 = 0;
        let x1 = 1;
        let y0 = 0;
        let y1 = 1;
        let width = 0;

        const addVertex = (x, y, u, v, r, g, b, a) => {
            vertexData[offset++] = x;
            vertexData[offset++] = y;
            vertexData[offset++] = u;
            vertexData[offset++] = v;
            vertexData[offset++] = r;
            vertexData[offset++] = g;
            vertexData[offset++] = b;
            vertexData[offset++] = a;
        };

        const spacing = 0.55;
        let colorNdx = 0;
        for (let i = 0; i < s.length; ++i) {
            const c = s.charCodeAt(i);
            if (c >= 32) { // Начало диапазона ASCII
                let cNdx = c - 32; // Корректируем индекс символа
                if (c >= 1040) { // Если это символ кириллицы
                    cNdx = c - 1040 + 95; // Корректируем индекс символа для кириллицы
                }
                const glyphX = cNdx % glyphsAcrossTexture;
                const glyphY = Math.floor(cNdx / glyphsAcrossTexture);
                const u0 = (glyphX * glyphWidth) / glyphCanvas.width;
                const v1 = (glyphY * glyphHeight) / glyphCanvas.height;
                const u1 = u0 + glyphUVWidth;
                const v0 = v1 + glyphUVheight;
                width = Math.max(x1, width);

                addVertex(x0, y0, u0, v0, ...colors[colorNdx]);
                addVertex(x1, y0, u1, v0, ...colors[colorNdx]);
                addVertex(x0, y1, u0, v1, ...colors[colorNdx]);
                addVertex(x1, y1, u1, v1, ...colors[colorNdx]);

                x0 = x0 + spacing;
                x1 = x0 + 1;
            }
            else {
                colorNdx = (colorNdx + 1) % colors.length;
                if (c === 10) {
                    x0 = 0;
                    x1 = 1;
                    y0 = y0 - 1;
                    y1 = y0 + 1;
                    continue;
                }
            }
            x0 = x0 + spacing;
            x1 = x0 + 1;
        }

        return {
            vertexData,
            numGlyphs: offset / floatsPerVertex,
            width,
            height: y1,
        };
    }

  
    var BooksInfo = AllLibraryInfo.AllBibliographicmaterial[0];
    var AuthorInfo = AllLibraryInfo.AllAuthors[0];
    var PublisherInfo = AllLibraryInfo.AllPublishers[0];
    let author = AuthorInfo.FullName;
    let date = BooksInfo.Date;
    let name = BooksInfo.Name;
    let  publisher = PublisherInfo.Name;

    const text = formatBookInfo(author, date, name, publisher)
    const { vertexData, numGlyphs, width, height } = generateGlyphVerticesForText(
        text, [
        [1, 1, 1, 1]

    ]);


    device.queue.writeBuffer(vertexBuffer, 0, vertexData);



    const pipeline = device.createRenderPipeline({
        label: 'hardcoded textured quad pipeline',
        layout: 'auto',
        vertex: {
            module,
            entryPoint: 'vs',
            buffers: [
                {
                    arrayStride: vertexSize,
                    attributes: [
                        { shaderLocation: 0, offset: 0, format: 'float32x2' },
                        { shaderLocation: 1, offset: 8, format: 'float32x2' },
                        { shaderLocation: 2, offset: 16, format: 'float32x4' },
                    ],
                },
            ],
        },
        fragment: {
            module,
            entryPoint: 'fs',
            targets: [
                {
                    format: presentationFormat,
                    blend: {
                        color: {
                            srcFactor: 'one',
                            dstFactor: 'one-minus-src-alpha',
                            operation: 'add',
                        },
                        alpha: {
                            srcFactor: 'one',
                            dstFactor: 'one-minus-src-alpha',
                            operation: 'add',
                        },
                    },
                },
            ],
        },
    });



    function copySourceToTexture(device, texture, source, { flipY } = {}) {
        device.queue.copyExternalImageToTexture(
            { source, flipY, },
            { texture, premultipliedAlpha: true },
            { width: source.width, height: source.height },
        );
    }


    function createTextureFromSource(device, source, options = {}) {
        const texture = device.createTexture({
            format: 'rgba8unorm',
            size: [source.width, source.height],
            usage: GPUTextureUsage.TEXTURE_BINDING |
                GPUTextureUsage.COPY_DST |
                GPUTextureUsage.RENDER_ATTACHMENT,
        });
        copySourceToTexture(device, texture, source, options);
        return texture;
    }
    submitButton.addEventListener('click', function (event) {
   
        const inputData = getInputData();

      
        sendDataToServer(inputData);
    });

    
    function sendDataToServer(data) {
        console.log(data);
        fetch('/Home/PageBibliographicmaterialAdminRedaction', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
           
            body: JSON.stringify(data)
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error('Ошибка при отправке данных');
                }
                return response.json();
            })
            .then(result => {
                console.log('Данные успешно отправлены на сервер:', result);
               
            })
            .catch(error => {
                console.error('Произошла ошибка при отправке данных:', error);
                
            });
    }

    //function getInputData() {
        
    //    const author = authorInput.value;
    //    const year = yearInput.value;
    //    const title = nameInput.value;
    //    const publisher = publisherInput.value;
    //    return { author, year, title, publisher };
    //}

    function getInputData() {

        //const author = authorInput.value;
        const date = yearInput.value;
        const name = nameInput.value;
        //const publisher = publisherInput.value;
        return { date, name };
    }
 


    const texture = createTextureFromSource(device, glyphCanvas, { mips: true });
    const sampler = device.createSampler({
        minFilter: 'linear',
        magFilter: 'linear',
    });



    const uniformBufferSize =
        16 * 4;
    const uniformBuffer = device.createBuffer({
        label: 'uniforms for quad',
        size: uniformBufferSize,
        usage: GPUBufferUsage.UNIFORM | GPUBufferUsage.COPY_DST,
    });



    const kMatrixOffset = 0;
    const uniformValues = new Float32Array(uniformBufferSize / 4);
    const matrix = uniformValues.subarray(kMatrixOffset, 16);


    const bindGroup = device.createBindGroup({
        layout: pipeline.getBindGroupLayout(0),
        entries: [
            { binding: 0, resource: sampler },
            { binding: 1, resource: texture.createView() },
            { binding: 2, resource: { buffer: uniformBuffer } },
        ],
    });


    const renderPassDescriptor = {
        label: 'our basic canvas renderPass',
        colorAttachments: [
            {

                clearValue: [0.3, 0.3, 0.3, 1],
                loadOp: 'clear',
                storeOp: 'store',
            },
        ],
    };



    function render(time) {
        time = 0;

        const fov = 60 * Math.PI / 180;
        const aspect = canvas.clientWidth / canvas.clientHeight;
        const zNear = 0.001;
        const zFar = 50;
        const projectionMatrix = mat4.perspective(fov, aspect, zNear, zFar);

        const cameraPosition = [0, 0, 5];
        const up = [0, 1, 0];
        const target = [0, 0, 0];
        const viewMatrix = mat4.lookAt(cameraPosition, target, up);
        const viewProjectionMatrix = mat4.multiply(projectionMatrix, viewMatrix);


        canvas.width = canvas.clientWidth;
        canvas.height = canvas.clientHeight;
        const scaleX = 0.5;
        //const scaleX =  desiredTextHeight / desiredTextWidth;
        const scaleY = 0.7;
        mat4.scale(viewProjectionMatrix, [scaleX, scaleY, 1], viewProjectionMatrix);

        renderPassDescriptor.colorAttachments[0].view =
            context.getCurrentTexture().createView();

        const encoder = device.createCommandEncoder({
            label: 'render quad encoder',
        });
        const pass = encoder.beginRenderPass(renderPassDescriptor);
        pass.setPipeline(pipeline);

        mat4.rotateY(viewProjectionMatrix, time, matrix);
        mat4.translate(matrix, [-width / 2, -height / 2, 0], matrix);

        device.queue.writeBuffer(uniformBuffer, 0, uniformValues);

        pass.setBindGroup(0, bindGroup);
        pass.setVertexBuffer(0, vertexBuffer);
        pass.setIndexBuffer(indexBuffer, 'uint32');
        pass.drawIndexed(numGlyphs * 6);

        pass.end();

        const commandBuffer = encoder.finish();
        device.queue.submit([commandBuffer]);

        requestAnimationFrame(render);

    }
    requestAnimationFrame(render);
}

function fail(msg) {
    alert(msg);
}
var AllLibraryInfo = booksData;
main(AllLibraryInfo);


