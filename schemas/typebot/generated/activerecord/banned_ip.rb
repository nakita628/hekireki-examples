class BannedIp < ApplicationRecord
  self.table_name = "BannedIp"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt

  validates :ip, presence: true, uniqueness: true

  belongs_to :responsible_typebot, class_name: "Typebot", foreign_key: "responsibleTypebotId", inverse_of: :banned_ips
  belongs_to :user, foreign_key: "userId", inverse_of: :banned_ips
end
