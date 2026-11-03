<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasOne;

class OrganisationAuthenticationPortal extends Model
{
    protected $table = 'OrganisationAuthenticationPortal';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'enabled',
        'clientId',
        'clientSecret',
        'wellKnownUrl',
        'defaultOrganisationRole',
        'autoProvisionUsers',
        'allowedDomains',
        'allowPersonalOrganisations',
    ];

    protected $attributes = [
        'enabled' => false,
        'clientId' => '',
        'clientSecret' => '',
        'wellKnownUrl' => '',
        'defaultOrganisationRole' => 'MEMBER',
        'autoProvisionUsers' => true,
        'allowPersonalOrganisations' => false,
    ];

    protected $casts = [
        'enabled' => 'boolean',
        'defaultOrganisationRole' => OrganisationMemberRole::class,
        'autoProvisionUsers' => 'boolean',
        'allowPersonalOrganisations' => 'boolean',
    ];

    public function organisation(): HasOne
    {
        return $this->hasOne(Organisation::class, 'organisationAuthenticationPortalId');
    }
}