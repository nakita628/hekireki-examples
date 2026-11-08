<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class ApiRequest extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'api_requests';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'method',
        'path',
        'statusCode',
        'duration',
        'projectId',
        'userId',
        'authType',
        'ip',
        'userAgent',
        'errorCode',
        'errorMessage',
        'requestSize',
        'responseSize',
    ];

    protected $casts = [
        'statusCode' => 'integer',
        'duration' => 'integer',
        'requestSize' => 'integer',
        'responseSize' => 'integer',
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'projectId');
    }
}