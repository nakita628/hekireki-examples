<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class DefaultDomains extends Model
{
    protected $table = 'DefaultDomains';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'dubsh',
        'chatgpt',
        'sptifi',
        'gitnew',
        'callink',
        'amznid',
        'ggllink',
        'figpage',
        'loooooooong',
        'projectId',
    ];

    protected $attributes = [
        'dubsh' => true,
        'chatgpt' => true,
        'sptifi' => true,
        'gitnew' => true,
        'callink' => true,
        'amznid' => true,
        'ggllink' => true,
        'figpage' => true,
        'loooooooong' => false,
    ];

    protected $casts = [
        'dubsh' => 'boolean',
        'chatgpt' => 'boolean',
        'sptifi' => 'boolean',
        'gitnew' => 'boolean',
        'callink' => 'boolean',
        'amznid' => 'boolean',
        'ggllink' => 'boolean',
        'figpage' => 'boolean',
        'loooooooong' => 'boolean',
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'projectId');
    }
}