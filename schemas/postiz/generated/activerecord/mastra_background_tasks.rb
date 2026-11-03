class MastraBackgroundTasks < ApplicationRecord
  alias_attribute :created_at, :createdAt

  validates :tool_call_id, presence: true
  validates :tool_name, presence: true
  validates :agent_id, presence: true
  validates :run_id, presence: true
  validates :status, presence: true
  validates :args, exclusion: { in: [ nil ] }
  validates :retry_count, presence: true
  validates :max_retries, presence: true
  validates :timeout_ms, presence: true
end
