defmodule LangWatch.SsoActivationRecoveryReservation do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          command_id: String.t(),
          organization_id: String.t(),
          connection_id: String.t(),
          created_at: DateTime.t()
        }

  schema "SsoActivationRecoveryReservation" do
    field(:command_id, :string, primary_key: true, source: :commandId)
    field(:organization_id, :string, source: :organizationId)
    field(:connection_id, :string, source: :connectionId)
    field(:created_at, LangWatch.PrismaDateTime, source: :createdAt)
  end
end