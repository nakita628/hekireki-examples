class EventData < ApplicationRecord
  self.primary_key = "event_data_id"

  validates :data_key, presence: true, length: { maximum: 500 }
  validates :string_value, length: { maximum: 500 }
  validates :data_type, presence: true

  belongs_to :website, inverse_of: :event_data
  belongs_to :website_event, inverse_of: :event_data
end
