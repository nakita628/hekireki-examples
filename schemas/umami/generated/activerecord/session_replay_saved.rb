class SessionReplaySaved < ApplicationRecord
  self.table_name = "session_replay_saved"
  self.primary_key = "saved_replay_id"

  validates :name, presence: true, length: { maximum: 100 }
  validates :visit_id, presence: true, uniqueness: { scope: :website_id }

  belongs_to :website, inverse_of: :session_replays_saved
end
