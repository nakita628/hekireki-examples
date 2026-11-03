class ComputedUpdatePauseScope < ApplicationRecord
  self.table_name = "computed_update_pause_scope"

  attribute :write_policy, default: "allow_bounded"

  validates :scope_type, presence: true, uniqueness: { scope: :scope_id }
  validates :scope_id, presence: true
end
