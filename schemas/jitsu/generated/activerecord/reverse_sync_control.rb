class ReverseSyncControl < ApplicationRecord
  self.table_name = "reverse_sync_control"
  self.primary_key = %w[workspace_id sync_id run_id]

  attribute :run_order, default: 0
  attribute :detached, default: false
  attribute :phase, default: "new"
  attribute :base_sequence, default: 0
  attribute :next_sequence, default: 0
  attribute :checkpoint_sequence, default: 0

  enum :mode, { upsert: "upsert", mirror: "mirror" }, prefix: true, validate: true
  enum :extraction, { cursor: "cursor", full: "full" }, validate: true

  validates :workspace_id, uniqueness: { scope: %i[sync_id run_order] }
  validates :revision, presence: true
  validates :target_hash, presence: true
end
