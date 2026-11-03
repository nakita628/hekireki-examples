class Session < ApplicationRecord
  validates :expires_at, presence: true
  validates :token, presence: true, uniqueness: true
  validates :token, uniqueness: true

  belongs_to :user, inverse_of: :sessions
  belongs_to :impersonated_by_user, class_name: "User", foreign_key: "impersonated_by", optional: true,
                                    inverse_of: :impersonated_sessions
end
