<?php

namespace App\Models;


class DatasetItemMedia extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    protected $table = 'dataset_item_media';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'project_id',
        'media_id',
        'dataset_id',
        'dataset_item_id',
        'dataset_item_valid_from',
        'field',
        'json_path',
        'reference_string',
    ];

    protected $casts = [
        'dataset_item_valid_from' => 'datetime',
    ];
}