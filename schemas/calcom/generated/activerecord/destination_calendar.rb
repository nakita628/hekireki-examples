class DestinationCalendar < ApplicationRecord
  self.table_name = "DestinationCalendar"

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :integration, presence: true
  validates :externalId, presence: true
  validates :userId, uniqueness: { allow_nil: true }
  validates :eventTypeId, uniqueness: { allow_nil: true }

  belongs_to :user, foreign_key: "userId", optional: true, inverse_of: :destination_calendar
  belongs_to :event_type, foreign_key: "eventTypeId", optional: true, inverse_of: :destination_calendar
  belongs_to :credential, foreign_key: "credentialId", optional: true, inverse_of: :destination_calendars
  belongs_to :delegation_credential, foreign_key: "delegationCredentialId", optional: true,
                                     inverse_of: :destination_calendar
  has_many :booking, foreign_key: "destinationCalendarId", inverse_of: :destination_calendar, dependent: :nullify
end
