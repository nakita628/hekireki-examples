class User < ApplicationRecord
  self.table_name = "User"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  attribute :isAdmin, default: false
  attribute :cancelAtPeriodEnd, default: false
  attribute :credits, default: 3
  alias_attribute :created_at, :createdAt

  validates :email, uniqueness: { allow_nil: true }
  validates :username, uniqueness: { allow_nil: true }
  validates :paymentProcessorUserId, uniqueness: { allow_nil: true }

  has_many :gpt_responses, foreign_key: "userId", inverse_of: :user, dependent: :restrict_with_error
  has_many :contact_form_messages, foreign_key: "userId", inverse_of: :user, dependent: :restrict_with_error
  has_many :tasks, foreign_key: "userId", inverse_of: :user, dependent: :restrict_with_error
  has_many :files, class_name: "FileRecord", foreign_key: "userId", inverse_of: :user, dependent: :restrict_with_error
end
