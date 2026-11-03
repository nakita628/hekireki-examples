class ComputedUpdateChangeFrontier < ApplicationRecord
  self.table_name = "computed_update_change_frontier"
  self.primary_key = %w[scope_id kind table_id record_id field_id]
end
