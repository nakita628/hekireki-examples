defmodule Calcom.OrganizationSettings do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}

  @type t :: %__MODULE__{
          id: integer(),
          is_organization_configured: boolean(),
          is_organization_verified: boolean(),
          org_auto_accept_email: String.t(),
          lock_event_type_creation_for_users: boolean(),
          admin_gets_no_slots_notification: boolean(),
          is_admin_reviewed: boolean(),
          is_admin_apienabled: boolean(),
          allow_seoindexing: boolean(),
          org_profile_redirects_to_verified_domain: boolean(),
          disable_phone_only_smsnotifications: boolean(),
          disable_autofill_on_booking_page: boolean(),
          org_auto_join_on_signup: boolean(),
          disable_attendee_confirmation_email: boolean(),
          disable_attendee_cancellation_email: boolean(),
          disable_attendee_rescheduled_email: boolean(),
          disable_attendee_request_email: boolean(),
          disable_attendee_reassigned_email: boolean(),
          disable_attendee_awaiting_payment_email: boolean(),
          disable_attendee_reschedule_request_email: boolean(),
          disable_attendee_location_change_email: boolean(),
          disable_attendee_new_event_email: boolean(),
          organization: Calcom.Team.t() | nil,
          d_sync_data: Calcom.DSyncData.t() | nil
        }

  schema "OrganizationSettings" do
    field(:is_organization_configured, :boolean, default: false, source: :isOrganizationConfigured)
    field(:is_organization_verified, :boolean, default: false, source: :isOrganizationVerified)
    field(:org_auto_accept_email, :string, source: :orgAutoAcceptEmail)
    field(:lock_event_type_creation_for_users, :boolean, default: false, source: :lockEventTypeCreationForUsers)
    field(:admin_gets_no_slots_notification, :boolean, default: false, source: :adminGetsNoSlotsNotification)
    field(:is_admin_reviewed, :boolean, default: false, source: :isAdminReviewed)
    field(:is_admin_apienabled, :boolean, default: false, source: :isAdminAPIEnabled)
    field(:allow_seoindexing, :boolean, default: false, source: :allowSEOIndexing)
    field(:org_profile_redirects_to_verified_domain, :boolean, default: false, source: :orgProfileRedirectsToVerifiedDomain)
    field(:disable_phone_only_smsnotifications, :boolean, default: false, source: :disablePhoneOnlySMSNotifications)
    field(:disable_autofill_on_booking_page, :boolean, default: false, source: :disableAutofillOnBookingPage)
    field(:org_auto_join_on_signup, :boolean, default: true, source: :orgAutoJoinOnSignup)
    field(:disable_attendee_confirmation_email, :boolean, default: false, source: :disableAttendeeConfirmationEmail)
    field(:disable_attendee_cancellation_email, :boolean, default: false, source: :disableAttendeeCancellationEmail)
    field(:disable_attendee_rescheduled_email, :boolean, default: false, source: :disableAttendeeRescheduledEmail)
    field(:disable_attendee_request_email, :boolean, default: false, source: :disableAttendeeRequestEmail)
    field(:disable_attendee_reassigned_email, :boolean, default: false, source: :disableAttendeeReassignedEmail)
    field(:disable_attendee_awaiting_payment_email, :boolean, default: false, source: :disableAttendeeAwaitingPaymentEmail)
    field(:disable_attendee_reschedule_request_email, :boolean, default: false, source: :disableAttendeeRescheduleRequestEmail)
    field(:disable_attendee_location_change_email, :boolean, default: false, source: :disableAttendeeLocationChangeEmail)
    field(:disable_attendee_new_event_email, :boolean, default: false, source: :disableAttendeeNewEventEmail)
    field(:organization_id, :id, source: :organizationId)
    belongs_to(:organization, Calcom.Team, foreign_key: :organization_id, define_field: false)
    has_one(:d_sync_data, Calcom.DSyncData, foreign_key: :organization_id, references: :organization_id)
  end
end