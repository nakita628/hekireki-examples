class User < ApplicationRecord
  self.table_name = "user"

  attribute :firstName, default: ""
  attribute :lastName, default: ""
  attribute :locale, default: "fr"
  attribute :banned, default: false
  attribute :isPremium, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :role, { admin: "admin", user: "user" }, default: :user, validate: { allow_nil: true }

  validates :name, presence: true
  validates :email, presence: true, uniqueness: true
  validates :emailVerified, inclusion: { in: [ true, false ] }

  has_many :sessions, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :accounts, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :feedbacks, class_name: "Feedbacks", foreign_key: "userId", inverse_of: :user, dependent: :nullify
  has_many :workout_session, foreign_key: "userId", inverse_of: :user, dependent: :restrict_with_error
  has_many :favorite_exercises, class_name: "UserFavoriteExercise", foreign_key: "userId", inverse_of: :user,
                                dependent: :destroy
  has_many :subscriptions, foreign_key: "userId", inverse_of: :user, dependent: :restrict_with_error
  has_many :licenses, foreign_key: "userId", inverse_of: :user, dependent: :nullify
  has_many :program_enrollments, class_name: "UserProgramEnrollment", foreign_key: "userId", inverse_of: :user,
                                 dependent: :destroy
end
