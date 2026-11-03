#nullable enable

namespace Calcom.Models;

public partial class OrganizationSettings
{
    public int Id { get; set; }

    public virtual Team Organization { get; set; } = null!;

    public int OrganizationId { get; set; }

    public bool IsOrganizationConfigured { get; set; } = false;

    public bool IsOrganizationVerified { get; set; } = false;

    public string OrgAutoAcceptEmail { get; set; } = null!;

    public bool LockEventTypeCreationForUsers { get; set; } = false;

    public bool AdminGetsNoSlotsNotification { get; set; } = false;

    public bool IsAdminReviewed { get; set; } = false;

    public virtual DSyncData? DSyncData { get; set; }

    public bool IsAdminAPIEnabled { get; set; } = false;

    public bool AllowSEOIndexing { get; set; } = false;

    public bool OrgProfileRedirectsToVerifiedDomain { get; set; } = false;

    public bool DisablePhoneOnlySMSNotifications { get; set; } = false;

    public bool DisableAutofillOnBookingPage { get; set; } = false;

    public bool OrgAutoJoinOnSignup { get; set; } = true;

    public bool DisableAttendeeConfirmationEmail { get; set; } = false;

    public bool DisableAttendeeCancellationEmail { get; set; } = false;

    public bool DisableAttendeeRescheduledEmail { get; set; } = false;

    public bool DisableAttendeeRequestEmail { get; set; } = false;

    public bool DisableAttendeeReassignedEmail { get; set; } = false;

    public bool DisableAttendeeAwaitingPaymentEmail { get; set; } = false;

    public bool DisableAttendeeRescheduleRequestEmail { get; set; } = false;

    public bool DisableAttendeeLocationChangeEmail { get; set; } = false;

    public bool DisableAttendeeNewEventEmail { get; set; } = false;
}
