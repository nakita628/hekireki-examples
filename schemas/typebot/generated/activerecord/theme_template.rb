class ThemeTemplate < ApplicationRecord
  self.table_name = "ThemeTemplate"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true
  validates :theme, exclusion: { in: [ nil ] }

  belongs_to :workspace, foreign_key: "workspaceId", inverse_of: :theme_templates
end
