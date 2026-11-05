class CodeMigration < ApplicationRecord
  self.table_name = "__code_migrations"

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true, uniqueness: true
end
