<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class Deployment extends Model
{
    use PrismaDates;

    protected $table = 'Deployment';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'logo',
        'theme',
        'licenseKey',
        'signatureTokenEncrypted',
        'agreedLicenseAt',
    ];

    protected $casts = [
        'theme' => 'array',
        'agreedLicenseAt' => 'datetime',
    ];
}