class ProfileBuilderState2 < ApplicationRecord
  self.table_name = "ProfileBuilderState2"
  self.primary_key = "profileBuilderId"

  alias_attribute :updated_at, :updatedAt
end
