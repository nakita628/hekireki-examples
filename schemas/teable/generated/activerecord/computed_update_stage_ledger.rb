class ComputedUpdateStageLedger < ApplicationRecord
  self.table_name = "computed_update_stage_ledger"
  self.primary_key = %w[scope_id kind table_id record_id]

  attribute :seq, default: 0
end
