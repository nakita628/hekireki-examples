class ReverseSyncTargetOwner < ApplicationRecord
  self.table_name = "reverse_sync_target_owner"
  self.primary_key = "target_hash"

  validates :workspace_id, presence: true, uniqueness: { scope: :sync_id }
  validates :sync_id, presence: true
end
