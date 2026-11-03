class User < ApplicationRecord
  self.table_name = "User"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :plan, default: "free"
  alias_attribute :created_at, :createdAt

  enum :referralTrack, { affiliate: "AFFILIATE", partner_discount: "PARTNER_DISCOUNT" }, validate: { allow_nil: true }

  validates :email, uniqueness: { allow_nil: true }
  validates :stripeId, uniqueness: { allow_nil: true }
  validates :subscriptionId, uniqueness: { allow_nil: true }

  has_many :accounts, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :sessions, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :documents, foreign_key: "ownerId", inverse_of: :owner, dependent: :nullify
  has_many :teams, class_name: "UserTeam", foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :user_datarooms, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :domains, foreign_key: "userId", inverse_of: :user, dependent: :nullify
  has_many :chats, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :restricted_tokens, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :participated_conversations, class_name: "ConversationParticipant", foreign_key: "userId", inverse_of: :user,
                                        dependent: :nullify
  has_many :messages, foreign_key: "userId", inverse_of: :user, dependent: :nullify
  has_many :created_dataroom_questions, class_name: "DataroomQuestion", foreign_key: "createdByUserId",
                                        inverse_of: :created_by_user, dependent: :nullify
  has_many :published_faq_items, class_name: "DataroomFaqItem", foreign_key: "publishedByUserId",
                                 inverse_of: :published_by_user, dependent: :destroy
  has_many :created_annotations, class_name: "DocumentAnnotation", foreign_key: "createdById", inverse_of: :created_by,
                                 dependent: :destroy
  has_many :created_redaction_jobs, class_name: "DocumentRedactionJob", foreign_key: "createdById",
                                    inverse_of: :created_by, dependent: :restrict_with_error
  has_many :installed_integrations, foreign_key: "userId", inverse_of: :user, dependent: :nullify
  has_many :links, foreign_key: "ownerId", inverse_of: :owner, dependent: :nullify
  has_many :frozen_datarooms, class_name: "Dataroom", foreign_key: "frozenBy", inverse_of: :frozen_by_user,
                              dependent: :nullify
  has_many :created_tasks, class_name: "Task", foreign_key: "createdByUserId", inverse_of: :created_by_user,
                           dependent: :nullify
  has_many :task_activities, foreign_key: "userId", inverse_of: :user, dependent: :nullify
  has_many :notification_preferences, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :referral_invites, foreign_key: "userId", inverse_of: :user, dependent: :destroy
end
