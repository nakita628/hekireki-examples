class AuthzedProjectionScopeState < ApplicationRecord
  self.table_name = "AuthzedProjectionScopeState"
  self.primary_key = "scope"

  alias_attribute :updated_at, :updatedAt
end
