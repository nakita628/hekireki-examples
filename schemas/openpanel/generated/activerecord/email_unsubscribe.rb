class EmailUnsubscribe < ApplicationRecord
  alias_attribute :created_at, :createdAt

  validates :email, presence: true, uniqueness: { scope: :category }
  validates :category, presence: true
end
