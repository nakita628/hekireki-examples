class CalVideoSettings < ApplicationRecord
  self.table_name = "CalVideoSettings"
  self.primary_key = "eventTypeId"

  attribute :disableRecordingForOrganizer, default: false
  attribute :disableRecordingForGuests, default: false
  attribute :enableAutomaticTranscription, default: false
  attribute :enableAutomaticRecordingForOrganizer, default: false
  attribute :disableTranscriptionForGuests, default: false
  attribute :disableTranscriptionForOrganizer, default: false
  attribute :requireEmailForGuests, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  belongs_to :event_type, foreign_key: "eventTypeId", inverse_of: :cal_video_settings
end
