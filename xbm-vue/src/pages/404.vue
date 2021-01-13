
<template>
  <div style="height:100%;width:100%">
    <ul class="box" ref="box">
      <li class="left" ref="left">侧边栏</li>
      <li class="resize" ref="resize"></li>
      <li class="mid" ref="mid">
        <div style="width:100%;height:100%">
          <iframe
            src="https://www.cnblogs.com/zoupufa/p/4789302.html"
            frameborder="0"
            width="100%"
            class="iframe-box"
            height="100%"
          ></iframe>
        </div>
      </li>
      <!-- <li class="resize2" ref="resize2"></li> -->
      <!-- <li class="right" ref="right">test</li> -->
    </ul>
  </div>
</template>


<script>
export default {
  mounted() {
    this.dragControllerDiv();
  },
  methods: {
    dragControllerDiv: function() {
      var resize = document.getElementsByClassName("resize");
      var left = document.getElementsByClassName("left");
      var mid = document.getElementsByClassName("mid");
      var box = document.getElementsByClassName("box");
      for (let i = 0; i < resize.length; i++) {
        resize[i].onmousedown = function(e) {
          var startX = e.clientX;
          resize[i].left = resize[i].offsetLeft;
          document.onmousemove = function(e) {
            var endX = e.clientX;
            var moveLen = resize[i].left + (endX - startX);
            var maxT = box[i].clientWidth - resize[i].offsetWidth;
            if (moveLen < 100) moveLen = 100;
            if (moveLen > 280) moveLen = 280;
            // if (moveLen > maxT - 280) moveLen = maxT - 280;

            resize[i].style.left = moveLen;

            for (let j = 0; j < left.length; j++) {
              left[j].style.width = moveLen + "px";
              mid[j].style.width = box[i].clientWidth - moveLen - 50 + "px";
            }
          };
          document.onmouseup = function(evt) {
            document.onmousemove = null;
            document.onmouseup = null;
            resize[i].releaseCapture && resize[i].releaseCapture();
          };
          resize[i].setCapture && resize[i].setCapture();
          return false;
        };
      }
    }
  }
};
</script>
<style scoped>
ul,
li {
  list-style: none;
  display: block;
  margin: 0;
  padding: 0;
}
.box {
  width: 100%;
  height: 100%;
  overflow: hidden;
}
.left {
  width: 280px;
  height: 100%;
  background: skyblue;
  float: left;
}

.resize {
  width: 50px;
  height: 100%;
  cursor: w-resize;
  float: left;
}
.mid {
  float: left;
  width: calc(100% - 340px);
  height: 100%;
  background: #f00;
}
.iframe-box {
  pointer-events: none;
}
</style>