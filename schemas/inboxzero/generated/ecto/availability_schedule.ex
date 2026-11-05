defmodule InboxZero.AvailabilitySchedule do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          is_default: boolean(),
          timezone: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          email_account: InboxZero.EmailAccount.t() | nil,
          windows: [InboxZero.AvailabilityWindow.t()]
        }

  schema "AvailabilitySchedule" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:is_default, :boolean, default: false, source: :isDefault)
    field(:timezone, :string, default: "UTC")
    field(:email_account_id, :string, source: :emailAccountId)
    belongs_to(:email_account, InboxZero.EmailAccount, foreign_key: :email_account_id, define_field: false, type: :string)
    has_many(:windows, InboxZero.AvailabilityWindow, foreign_key: :availability_schedule_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end