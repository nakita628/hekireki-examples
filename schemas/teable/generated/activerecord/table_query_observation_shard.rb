class TableQueryObservationShard < ApplicationRecord
  self.table_name = "table_query_observation_shard"
  self.primary_key = %w[table_id query_kind shape_hash window_start writer_id]

  validates :base_id, presence: true
  validates :window_size_seconds, presence: true
  validates :request_count, presence: true
  validates :slow_count, presence: true
  validates :timeout_count, presence: true
  validates :db_error_count, presence: true
  validates :total_duration_ms, presence: true
  validates :max_duration_ms, presence: true
  validates :shape, exclusion: { in: [ nil ] }
end
