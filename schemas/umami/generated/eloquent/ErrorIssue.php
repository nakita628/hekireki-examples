<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class ErrorIssue extends Model
{
    use PrismaDates;

    const UPDATED_AT = null;
    const ZONED_DATES = ['resolved_at', 'created_at'];

    protected $table = 'error_issue';

    protected $primaryKey = 'issue_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'issue_id',
        'website_id',
        'fingerprint',
        'grouping_version',
        'title',
        'status',
        'resolved_at',
    ];

    protected $attributes = [
        'grouping_version' => 1,
        'status' => 'unresolved',
    ];

    protected $casts = [
        'grouping_version' => 'integer',
        'resolved_at' => 'datetime',
    ];
}