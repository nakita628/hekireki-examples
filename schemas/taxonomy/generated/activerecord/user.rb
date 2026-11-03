class User < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }

  validates :email, uniqueness: { allow_nil: true }
  validates :stripe_customer_id, uniqueness: { allow_nil: true }
  validates :stripe_subscription_id, uniqueness: { allow_nil: true }

  has_many :accounts, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :sessions, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :post, class_name: "Post", foreign_key: "authorId", inverse_of: :author, dependent: :restrict_with_error
end
