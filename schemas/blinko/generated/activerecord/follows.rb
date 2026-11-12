class Follows < ApplicationRecord
  attribute :followType, default: "following"
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :siteUrl, presence: true

  belongs_to :account, class_name: "Accounts", foreign_key: "accountId", inverse_of: :follows
end
