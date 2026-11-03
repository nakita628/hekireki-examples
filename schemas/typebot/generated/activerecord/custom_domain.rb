class CustomDomain < ApplicationRecord
  self.table_name = "CustomDomain"
  self.primary_key = "name"

  alias_attribute :created_at, :createdAt

  belongs_to :workspace, foreign_key: "workspaceId", inverse_of: :custom_domains
end
