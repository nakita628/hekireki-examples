class CustomFieldResponse < ApplicationRecord
  self.table_name = "CustomFieldResponse"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :data, exclusion: { in: [ nil ] }
  validates :viewId, uniqueness: true

  belongs_to :view, foreign_key: "viewId", inverse_of: :custom_field_response
end
