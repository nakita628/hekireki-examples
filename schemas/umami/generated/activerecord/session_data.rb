class SessionData < ApplicationRecord
  self.primary_key = "session_data_id"

  validates :data_key, presence: true, length: { maximum: 500 }, uniqueness: { scope: :session_id }
  validates :string_value, length: { maximum: 500 }
  validates :data_type, presence: true
  validates :distinct_id, length: { maximum: 50 }

  belongs_to :website, inverse_of: :session_data
  belongs_to :session, inverse_of: :session_data
end
