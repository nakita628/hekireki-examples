class SelectedCalendar < ApplicationRecord
  self.table_name = "SelectedCalendar"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  attribute :syncSubscribedErrorCount, default: 0
  attribute :syncErrorCount, default: 0
  attribute :watchAttempts, default: 0
  attribute :unwatchAttempts, default: 0
  attribute :maxAttempts, default: 3
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :integration, presence: true,
                          uniqueness: { scope: [ :userId, :externalId, :eventTypeId ], conditions: -> { where.not(eventTypeId: nil) } }
  validates :externalId, presence: true
  validates :googleChannelId,
            uniqueness: { scope: :eventTypeId, conditions: -> { where.not(eventTypeId: nil) }, allow_nil: true }

  belongs_to :user, foreign_key: "userId", inverse_of: :selected_calendars
  belongs_to :credential, foreign_key: "credentialId", optional: true, inverse_of: :selected_calendars
  belongs_to :delegation_credential, foreign_key: "delegationCredentialId", optional: true,
                                     inverse_of: :selected_calendars
  belongs_to :event_type, foreign_key: "eventTypeId", optional: true, inverse_of: :selected_calendars
  has_many :calendar_cache_events, foreign_key: "selectedCalendarId", inverse_of: :selected_calendar,
                                   dependent: :destroy
end
