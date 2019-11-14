<template>
  <div id="app">
    <Header />
    <router-view/>
  </div>
</template>

<script>
import Header from './components/layout/Header';
export default {
  name:"app",
  components: {
    Header
  }
}
</script>

<style>
  /* #app {} */
  *{
    box-sizing: border-box;
    margin: 0;
    padding: 0;
  }

  body{
    font-family: Arial, Helvetica, sans-serif;
    line-height: 1.4;
  }

  .btn{    
    border: none;
    background: #555;
    color: #fff;
    padding: 7px 20px;
    cursor: pointer;
  }

  .btn:hover{
    background: rgb(114, 113, 113);
  }

  .isRed{
    background: #f03c3c;
  }  
  .isRed:hover{
    background: #db6969;
  }
  .isYellow{
    background: #f1f12d;
  }  
  .isGreen{
    background: #29ff29;
  }
  .isGrey{
    background: #bbb9b9;
  }
</style>
