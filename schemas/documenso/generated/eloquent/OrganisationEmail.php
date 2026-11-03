<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class OrganisationEmail extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'OrganisationEmail';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'email',
        'emailName',
        'emailDomainId',
        'organisationId',
    ];

    public function emailDomain(): BelongsTo
    {
        return $this->belongsTo(EmailDomain::class, 'emailDomainId');
    }

    public function organisation(): BelongsTo
    {
        return $this->belongsTo(Organisation::class, 'organisationId');
    }

    public function organisationGlobalSettings(): HasMany
    {
        return $this->hasMany(OrganisationGlobalSettings::class, 'emailId');
    }

    public function teamGlobalSettings(): HasMany
    {
        return $this->hasMany(TeamGlobalSettings::class, 'emailId');
    }
}