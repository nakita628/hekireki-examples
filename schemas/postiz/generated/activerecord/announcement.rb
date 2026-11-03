class Announcement < ApplicationRecord
  self.table_name = "Announcement"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  alias_attribute :created_at, :createdAt

  enum :color, { info: "INFO", warning: "WARNING", error: "ERROR" }, default: :info, validate: true

  validates :title, presence: true
  validates :description, presence: true
end
