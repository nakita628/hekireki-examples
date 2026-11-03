<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasOne;

class OrganizationSettings extends Model
{
    protected $table = 'OrganizationSettings';

    public $timestamps = false;

    protected $fillable = [
        'organizationId',
        'isOrganizationConfigured',
        'isOrganizationVerified',
        'orgAutoAcceptEmail',
        'lockEventTypeCreationForUsers',
        'adminGetsNoSlotsNotification',
        'isAdminReviewed',
        'isAdminAPIEnabled',
        'allowSEOIndexing',
        'orgProfileRedirectsToVerifiedDomain',
        'disablePhoneOnlySMSNotifications',
        'disableAutofillOnBookingPage',
        'orgAutoJoinOnSignup',
        'disableAttendeeConfirmationEmail',
        'disableAttendeeCancellationEmail',
        'disableAttendeeRescheduledEmail',
        'disableAttendeeRequestEmail',
        'disableAttendeeReassignedEmail',
        'disableAttendeeAwaitingPaymentEmail',
        'disableAttendeeRescheduleRequestEmail',
        'disableAttendeeLocationChangeEmail',
        'disableAttendeeNewEventEmail',
    ];

    protected $attributes = [
        'isOrganizationConfigured' => false,
        'isOrganizationVerified' => false,
        'lockEventTypeCreationForUsers' => false,
        'adminGetsNoSlotsNotification' => false,
        'isAdminReviewed' => false,
        'isAdminAPIEnabled' => false,
        'allowSEOIndexing' => false,
        'orgProfileRedirectsToVerifiedDomain' => false,
        'disablePhoneOnlySMSNotifications' => false,
        'disableAutofillOnBookingPage' => false,
        'orgAutoJoinOnSignup' => true,
        'disableAttendeeConfirmationEmail' => false,
        'disableAttendeeCancellationEmail' => false,
        'disableAttendeeRescheduledEmail' => false,
        'disableAttendeeRequestEmail' => false,
        'disableAttendeeReassignedEmail' => false,
        'disableAttendeeAwaitingPaymentEmail' => false,
        'disableAttendeeRescheduleRequestEmail' => false,
        'disableAttendeeLocationChangeEmail' => false,
        'disableAttendeeNewEventEmail' => false,
    ];

    protected $casts = [
        'organizationId' => 'integer',
        'isOrganizationConfigured' => 'boolean',
        'isOrganizationVerified' => 'boolean',
        'lockEventTypeCreationForUsers' => 'boolean',
        'adminGetsNoSlotsNotification' => 'boolean',
        'isAdminReviewed' => 'boolean',
        'isAdminAPIEnabled' => 'boolean',
        'allowSEOIndexing' => 'boolean',
        'orgProfileRedirectsToVerifiedDomain' => 'boolean',
        'disablePhoneOnlySMSNotifications' => 'boolean',
        'disableAutofillOnBookingPage' => 'boolean',
        'orgAutoJoinOnSignup' => 'boolean',
        'disableAttendeeConfirmationEmail' => 'boolean',
        'disableAttendeeCancellationEmail' => 'boolean',
        'disableAttendeeRescheduledEmail' => 'boolean',
        'disableAttendeeRequestEmail' => 'boolean',
        'disableAttendeeReassignedEmail' => 'boolean',
        'disableAttendeeAwaitingPaymentEmail' => 'boolean',
        'disableAttendeeRescheduleRequestEmail' => 'boolean',
        'disableAttendeeLocationChangeEmail' => 'boolean',
        'disableAttendeeNewEventEmail' => 'boolean',
    ];

    public function organization(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'organizationId');
    }

    public function dSyncData(): HasOne
    {
        return $this->hasOne(DSyncData::class, 'organizationId', 'organizationId');
    }
}