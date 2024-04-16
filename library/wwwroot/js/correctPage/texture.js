﻿function renderQuad(context, device, pipeline, bindGroups, renderPassDescriptor, settings) {
    const ndx = (settings.addressModeU === 'repeat' ? 1 : 0) +
        (settings.addressModeV === 'repeat' ? 2 : 0) +
        (settings.magFilter === 'linear' ? 4 : 0);
    const bindGroup = bindGroups[ndx];

    renderPassDescriptor.colorAttachments[0].view =
        context.getCurrentTexture().createView();

    const encoder = device.createCommandEncoder({
        label: 'render quad encoder',
    });

    const pass = encoder.beginRenderPass(renderPassDescriptor);
    pass.setPipeline(pipeline);
    pass.setBindGroup(0, bindGroup);
    pass.draw(6);  // call our vertex shader 6 times
    pass.end();

    const commandBuffer = encoder.finish();
    device.queue.submit([commandBuffer]);
}

async function main() {
    const adapter = await navigator.gpu?.requestAdapter();
    const device = await adapter?.requestDevice();
    if (!device) {
        fail('need a browser that supports WebGPU');
        return;
    }

    // Get a WebGPU context from the canvas and configure it
    const canvas = document.createElement('canvas');

    // Устанавливаем его размеры
    canvas.width = 450; // Примерное значение ширины
    canvas.height = 600; // Примерное значение высоты
    canvas.style.border = '1px solid black';
    canvas.style.position = 'absolute';
    canvas.style.top = '30%';
    canvas.style.left = '88%';
    canvas.style.transform = 'translate(-50%, -50%)';
    canvas.classList.add('myCanvasClass');


    const submitButton = document.createElement('button');
    submitButton.textContent = 'X'; // Устанавливаем текст на кнопке
    submitButton.style.position = 'absolute'; // Устанавливаем позиционирование
    submitButton.style.top = '1%'; // Устанавливаем отступ сверху
    submitButton.style.left = '77%'; // Устанавливаем отступ слева
    submitButton.style.zIndex = '1';
    submitButton.classList.add('myCanvasClass');

    // Добавляем canvas на страницу, например, в body
    document.body.appendChild(canvas);
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
      struct OurVertexShaderOutput {
        @builtin(position) position: vec4f,
        @location(0) texcoord: vec2f,
      };

      @vertex fn vs(
        @builtin(vertex_index) vertexIndex : u32
      ) -> OurVertexShaderOutput {
        let pos = array(
          // 1st triangle
            vec2f(0, 1),    // верхний левый угол
            vec2f(0, 0),    // нижний левый угол
            vec2f(1, 0),    // нижний правый угол

            // 2nd triangle
            vec2f(0, 1),    // верхний левый угол
            vec2f(1, 0),    // нижний правый угол
            vec2f(1, 1)     // верхний правый угол
        );

        var vsOutput: OurVertexShaderOutput;
        let xy = pos[vertexIndex];
        vsOutput.position = vec4f(xy, 0.0, 1.0);
        vsOutput.texcoord = xy;
        return vsOutput;
      }

      @group(0) @binding(0) var ourSampler: sampler;
      @group(0) @binding(1) var ourTexture: texture_2d<f32>;

      @fragment fn fs(fsInput: OurVertexShaderOutput) -> @location(0) vec4f {
        return textureSample(ourTexture, ourSampler, fsInput.texcoord);
      }
    `,
    });

    const pipeline = device.createRenderPipeline({
        label: 'hardcoded textured quad pipeline',
        layout: 'auto',
        vertex: {
            module,
            entryPoint: 'vs',
        },
        fragment: {
            module,
            entryPoint: 'fs',
            targets: [{ format: presentationFormat }],
        },
    });

    async function loadImageBitmap(url) {
        const res = await fetch(url);
        const blob = await res.blob();
        return await createImageBitmap(blob, { colorSpaceConversion: 'none' });
    }

    const url = '/img/pict1.jpg';
    const source = await loadImageBitmap(url);
    const texture = device.createTexture({
        label: url,
        format: 'rgba8unorm',
        size: [source.width, source.height],
        usage: GPUTextureUsage.TEXTURE_BINDING |
            GPUTextureUsage.COPY_DST |
            GPUTextureUsage.RENDER_ATTACHMENT,
    });
    device.queue.copyExternalImageToTexture(
        { source, flipY: true },
        { texture },
        { width: source.width, height: source.height },
    );

    const bindGroups = [];
    for (let i = 0; i < 8; ++i) {
        const sampler = device.createSampler({
            addressModeU: (i & 1) ? 'repeat' : 'clamp-to-edge',
            addressModeV: (i & 2) ? 'repeat' : 'clamp-to-edge',
            magFilter: (i & 4) ? 'linear' : 'nearest',
        });

        const bindGroup = device.createBindGroup({
            layout: pipeline.getBindGroupLayout(0),
            entries: [
                { binding: 0, resource: sampler },
                { binding: 1, resource: texture.createView() },
            ],
        });
        bindGroups.push(bindGroup);
    }

    const renderPassDescriptor = {
        label: 'our basic canvas renderPass',
        colorAttachments: [
            {
                // view: <- to be filled out when we render
                clearValue: [0.3, 0.3, 0.3, 1],
                loadOp: 'clear',
                storeOp: 'store',
            },
        ],
    };

    const settings = {
        addressModeU: 'repeat',
        addressModeV: 'repeat',
        magFilter: 'linear',
    };
    const canvasElements = document.getElementsByClassName('myCanvasClass');

    submitButton.addEventListener('click', function () {
        // Удаляем элементы с классом 'myCanvasClass'
        const elementsToRemove = document.querySelectorAll('.myCanvasClass');
        elementsToRemove.forEach(element => element.remove());
    });
    function render() {
        renderQuad(context, device, pipeline, bindGroups, renderPassDescriptor, settings);
    }


    render();
}

function fail(msg) {
    // eslint-disable-next-line no-alert
    alert(msg);
}

main();