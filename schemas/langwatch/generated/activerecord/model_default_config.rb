class ModelDefaultConfig < ApplicationRecord
  self.table_name = "ModelDefaultConfig"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :config, exclusion: { in: [ nil ] }
  validates :organizationId, presence: true

  has_many :scopes, class_name: "ModelDefaultConfigScope", foreign_key: "configId", inverse_of: :config,
                    dependent: :destroy
end
