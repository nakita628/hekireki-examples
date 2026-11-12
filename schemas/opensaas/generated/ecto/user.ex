defmodule OpenSaas.User do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: OpenSaas.PrismaDateTime, autogenerate: {OpenSaas.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          email: String.t() | nil,
          username: String.t() | nil,
          is_admin: boolean(),
          payment_processor_user_id: String.t() | nil,
          subscription_status: String.t() | nil,
          subscription_plan: String.t() | nil,
          current_period_end: DateTime.t() | nil,
          cancel_at_period_end: boolean(),
          date_paid: DateTime.t() | nil,
          credits: integer(),
          inserted_at: DateTime.t(),
          gpt_responses: [OpenSaas.GptResponse.t()],
          contact_form_messages: [OpenSaas.ContactFormMessage.t()],
          tasks: [OpenSaas.Task.t()],
          files: [OpenSaas.File.t()]
        }

  schema "User" do
    field(:email, :string)
    field(:username, :string)
    field(:is_admin, :boolean, default: false, source: :isAdmin)
    field(:payment_processor_user_id, :string, source: :paymentProcessorUserId)
    field(:subscription_status, :string, source: :subscriptionStatus)
    field(:subscription_plan, :string, source: :subscriptionPlan)
    field(:current_period_end, OpenSaas.PrismaDateTime, source: :currentPeriodEnd)
    field(:cancel_at_period_end, :boolean, default: false, source: :cancelAtPeriodEnd)
    field(:date_paid, OpenSaas.PrismaDateTime, source: :datePaid)
    field(:credits, :integer, default: 3)
    has_many(:gpt_responses, OpenSaas.GptResponse, foreign_key: :user_id)
    has_many(:contact_form_messages, OpenSaas.ContactFormMessage, foreign_key: :user_id)
    has_many(:tasks, OpenSaas.Task, foreign_key: :user_id)
    has_many(:files, OpenSaas.File, foreign_key: :user_id)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end