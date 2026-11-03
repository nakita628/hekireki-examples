class OrganizationSettings < ApplicationRecord
  self.table_name = "OrganizationSettings"

  attribute :isOrganizationConfigured, default: false
  attribute :isOrganizationVerified, default: false
  attribute :lockEventTypeCreationForUsers, default: false
  attribute :adminGetsNoSlotsNotification, default: false
  attribute :isAdminReviewed, default: false
  attribute :isAdminAPIEnabled, default: false
  attribute :allowSEOIndexing, default: false
  attribute :orgProfileRedirectsToVerifiedDomain, default: false
  attribute :disablePhoneOnlySMSNotifications, default: false
  attribute :disableAutofillOnBookingPage, default: false
  attribute :orgAutoJoinOnSignup, default: true
  attribute :disableAttendeeConfirmationEmail, default: false
  attribute :disableAttendeeCancellationEmail, default: false
  attribute :disableAttendeeRescheduledEmail, default: false
  attribute :disableAttendeeRequestEmail, default: false
  attribute :disableAttendeeReassignedEmail, default: false
  attribute :disableAttendeeAwaitingPaymentEmail, default: false
  attribute :disableAttendeeRescheduleRequestEmail, default: false
  attribute :disableAttendeeLocationChangeEmail, default: false
  attribute :disableAttendeeNewEventEmail, default: false

  validates :organizationId, uniqueness: true
  validates :orgAutoAcceptEmail, presence: true

  belongs_to :organization, class_name: "Team", foreign_key: "organizationId", inverse_of: :organization_settings
  has_one :d_sync_data, foreign_key: "organizationId", primary_key: "organizationId", inverse_of: :org,
                        dependent: :destroy
end
