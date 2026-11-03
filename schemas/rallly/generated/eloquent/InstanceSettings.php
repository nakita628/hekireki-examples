<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class InstanceSettings extends Model
{
    use PrismaDates;

    protected $table = 'instance_settings';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'disable_user_registration',
        'instance_id',
        'app_name',
        'primary_color',
        'primary_color_dark',
        'logo',
        'logo_dark',
        'logo_icon',
        'hide_attribution',
        'footer_links',
    ];

    protected $attributes = [
        'disable_user_registration' => false,
        'footer_links' => '[]',
    ];

    protected $casts = [
        'disable_user_registration' => 'boolean',
        'hide_attribution' => 'boolean',
        'footer_links' => 'array',
    ];
}