class ModelRouterRules < ApplicationRecord
  self.inheritance_column = nil

  attribute :enabled, default: true
  attribute :type, default: "calculated"
  attribute :createdAt, PrismaDateTime.new, default: -> { Time.current }
  attribute :lastUpdatedAt, PrismaDateTime.new, default: -> { Time.current }
  alias_attribute :created_at, :createdAt

  validates :priority, presence: true
  validates :title, presence: true, uniqueness: { scope: :router_id }
  validates :route_provider, presence: true
  validates :route_model, presence: true

  belongs_to :router, class_name: "ModelRouters", inverse_of: :rules
end
