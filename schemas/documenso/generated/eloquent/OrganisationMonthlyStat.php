<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class OrganisationMonthlyStat extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'OrganisationMonthlyStat';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'organisationId',
        'period',
        'documentCount',
        'emailCount',
        'apiCount',
        'emailReports',
    ];

    protected $attributes = [
        'documentCount' => 0,
        'emailCount' => 0,
        'apiCount' => 0,
        'emailReports' => 0,
    ];

    protected $casts = [
        'documentCount' => 'integer',
        'emailCount' => 'integer',
        'apiCount' => 'integer',
        'emailReports' => 'integer',
    ];

    public function organisation(): BelongsTo
    {
        return $this->belongsTo(Organisation::class, 'organisationId');
    }
}