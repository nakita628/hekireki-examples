class WaitpointTag < ApplicationRecord
  self.table_name = "WaitpointTag"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt

  validates :name, presence: true, uniqueness: { scope: :environmentId }

  belongs_to :environment, class_name: "RuntimeEnvironment", foreign_key: "environmentId", inverse_of: :waitpoint_tags
  belongs_to :project, foreign_key: "projectId", inverse_of: :waitpoint_tags
end
