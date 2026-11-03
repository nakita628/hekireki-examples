class TagsOnAccounts < ApplicationRecord
  self.table_name = "TagsOnAccounts"
  self.primary_key = %w[accountId tagId userId]

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  belongs_to :account, foreign_key: [ :accountId, :userId ], inverse_of: :tags
  belongs_to :tag, foreign_key: "tagId", inverse_of: :accounts
end
