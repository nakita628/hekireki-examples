class SessionReplay < ApplicationRecord
  self.table_name = "session_replay"
  self.primary_key = "replay_id"

  validates :session_id, presence: true
  validates :visit_id, presence: true
  validates :chunk_index, presence: true
  validates :events, exclusion: { in: [ nil ] }
  validates :event_count, presence: true
  validates :started_at, presence: true
  validates :ended_at, presence: true

  belongs_to :website, inverse_of: :session_replays
end
