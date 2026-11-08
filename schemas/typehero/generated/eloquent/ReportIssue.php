<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class ReportIssue extends Model
{
    protected $table = 'ReportIssue';

    public $timestamps = false;

    protected $fillable = [
        'reportId',
        'type',
    ];

    protected $casts = [
        'reportId' => 'integer',
        'type' => IssueType::class,
    ];

    public function report(): BelongsTo
    {
        return $this->belongsTo(Report::class, 'reportId');
    }
}