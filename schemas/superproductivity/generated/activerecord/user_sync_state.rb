class UserSyncState < ApplicationRecord
  self.table_name = "user_sync_state"
  self.primary_key = "user_id"

  attribute :last_seq, default: 0
  attribute :snapshot_schema_version, default: 1

  belongs_to :user, inverse_of: :sync_state
end
