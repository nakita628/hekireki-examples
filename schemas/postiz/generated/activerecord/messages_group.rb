class MessagesGroup < ApplicationRecord
  self.table_name = "MessagesGroup"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :buyerId, uniqueness: { scope: :sellerId }

  belongs_to :buyer, class_name: "User", foreign_key: "buyerId", inverse_of: :group_buyer
  belongs_to :buyer_organization, class_name: "Organization", foreign_key: "buyerOrganizationId",
                                  inverse_of: :buyer_organization
  belongs_to :seller, class_name: "User", foreign_key: "sellerId", inverse_of: :group_seller
  has_many :messages, foreign_key: "groupId", inverse_of: :group, dependent: :restrict_with_error
  has_many :orders, foreign_key: "messageGroupId", inverse_of: :message_group, dependent: :restrict_with_error
end
