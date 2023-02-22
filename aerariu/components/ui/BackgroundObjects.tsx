import React from 'react';
import style from './BackgroundObjects.module.css';

const BackgroundObjects = () => {
  return (
    <div className={[style.container, 'select-none'].join(' ')}>
      <div
        className={[
          style.object,
          style.keyEsc,
          'bg-pink-50 font-display text-pink-100',
        ].join(' ')}
      >
        <span className={style.objectFont}>ESC</span>
      </div>
      <div
        className={[
          style.object,
          style.keyEnter,
          'bg-pink-50 font-display text-pink-100',
        ].join(' ')}
      >
        <span className={style.objectFont}>ENTER</span>
      </div>
      <div
        className={[
          style.object,
          style.keySpacebar,
          'bg-pink-50 font-display text-pink-100',
        ].join(' ')}
      >
        <span className={style.objectFont}>SPACEBAR</span>
      </div>
      <div
        className={[
          style.object,
          style.keyW,
          'bg-pink-50 font-display text-pink-100',
        ].join(' ')}
      >
        <span className={style.objectFont}>W</span>
      </div>
      <div
        className={[
          style.object,
          style.keyA,
          'bg-pink-50 font-display text-pink-100',
        ].join(' ')}
      >
        <span className={style.objectFont}>A</span>
      </div>
      <div
        className={[
          style.object,
          style.keyS,
          'bg-pink-50 font-display text-pink-100',
        ].join(' ')}
      >
        <span className={style.objectFont}>S</span>
      </div>
      <div
        className={[
          style.object,
          style.keyD,
          'bg-pink-50 font-display text-pink-100',
        ].join(' ')}
      >
        <span className={style.objectFont}>D</span>
      </div>
    </div>
  );
};

export default BackgroundObjects;
