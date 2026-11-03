<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;

class WebCalloutEndpoint extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    protected $table = 'web_callout_endpoints';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'project_id',
        'name',
        'url',
        'enabled',
        'toast_message',
        'request_headers',
        'request_header_keys',
    ];

    protected $attributes = [
        'name' => 'Default',
        'enabled' => true,
        'toast_message' => 'Callout sent',
    ];

    protected $casts = [
        'enabled' => 'boolean',
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'project_id');
    }
}