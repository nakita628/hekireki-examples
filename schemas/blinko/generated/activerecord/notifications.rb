class Notifications < ApplicationRecord
  self.inheritance_column = nil

  attribute :isRead, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :type, presence: true
  validates :title, presence: true
  validates :content, presence: true

  belongs_to :account, class_name: "Accounts", foreign_key: "accountId", inverse_of: :notifications
end
