defmodule Blinko.Accounts do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Blinko.PrismaDateTime, autogenerate: {Blinko.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          name: String.t(),
          nickname: String.t(),
          password: String.t(),
          image: String.t(),
          api_token: String.t(),
          description: String.t(),
          note: integer(),
          role: String.t(),
          login_type: String.t(),
          link_account_id: integer() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          notes: [Blinko.Notes.t()],
          configs: [Blinko.Config.t()],
          tags: [Blinko.Tag.t()],
          comments: [Blinko.Comments.t()],
          attachments: [Blinko.Attachments.t()],
          follows: [Blinko.Follows.t()],
          notifications: [Blinko.Notifications.t()],
          conversations: [Blinko.Conversation.t()],
          shared_notes: [Blinko.NoteInternalShare.t()],
          ai_scheduled_tasks: [Blinko.AiScheduledTask.t()]
        }

  schema "accounts" do
    field(:name, :string, default: "")
    field(:nickname, :string, default: "")
    field(:password, :string, default: "")
    field(:image, :string, default: "")
    field(:api_token, :string, default: "", source: :apiToken)
    field(:description, :string, default: "")
    field(:note, :integer, default: 0)
    field(:role, :string, default: "")
    field(:login_type, :string, default: "", source: :loginType)
    field(:link_account_id, :integer, source: :linkAccountId)
    has_many(:notes, Blinko.Notes, foreign_key: :account_id)
    has_many(:configs, Blinko.Config, foreign_key: :user_id)
    has_many(:tags, Blinko.Tag, foreign_key: :account_id)
    has_many(:comments, Blinko.Comments, foreign_key: :account_id)
    has_many(:attachments, Blinko.Attachments, foreign_key: :account_id)
    has_many(:follows, Blinko.Follows, foreign_key: :account_id)
    has_many(:notifications, Blinko.Notifications, foreign_key: :account_id)
    has_many(:conversations, Blinko.Conversation, foreign_key: :account_id)
    has_many(:shared_notes, Blinko.NoteInternalShare, foreign_key: :account_id)
    has_many(:ai_scheduled_tasks, Blinko.AiScheduledTask, foreign_key: :account_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end